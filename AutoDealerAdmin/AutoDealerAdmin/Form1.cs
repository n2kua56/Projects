using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AutoDealerAdmin
{
    //TODO: Fix the grid column resizes - Works until the form has been resized.
    //TODO: Fill-in picture count for small pictures
    //TODO: Fill-in picture count for large pictures
    //TODO: Fill-in picture count for gallary pictures

    public partial class Form1 : Form
    {
        private MySqlConnection mConn = null;
        private int mLastId = -1;
        
        private int mRecNo = -1;
        double[] mCategoryWidths;
        double[] mVehicleWidths;

        private int mMinWidth = -1;
        private int mMinHeight = -1;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            string ConnStr = "";
            try
            {
                mMinWidth = this.Width;
                mMinHeight = this.Height;

                ConnStr = AutoDealerAdmin.Settings1.Default.connStr;
                ConnStr = ConnStr.Replace("{server}", Settings1.Default.server.Trim());
                ConnStr = ConnStr.Replace("{db}", Settings1.Default.db.Trim());
                ConnStr = ConnStr.Replace("{uid}", Settings1.Default.uid.Trim());
                ConnStr = ConnStr.Replace("{password}", Settings1.Default.password.Trim());
                mConn = new MySqlConnection(ConnStr);
                mConn.Open();
                zFillGrid();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connect to MySQL\n\n" +
                    "  ConnStr: " + ConnStr + "\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Width < mMinWidth) { this.Width = mMinWidth; }
            if (this.Height < mMinHeight) { this.Height = mMinHeight; }

            zResizeCategoryColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillGrid()
        {
            try
            {
                zFillCategories();
                zFillVehicles();
                zFillProperties();
                zFillSettings();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to fill-in the Grids\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillProperties()
        {
            try
            {
                string sql = "SELECT * " +
                                "FROM property " +
                                "ORDER BY Name; ";
                DataTable dt = zGetDataTable(sql, null);
                dgvProperties.DataSource = dt;
                dgvProperties.Columns[0].Visible = false;
                dgvProperties.Columns["Value"].Width = 200;
                dgvProperties.Columns["Description"].Width = 200;
                dgvProperties.Columns["PageDescription"].Width = 200;
                dgvProperties.Columns["Name"].Width = 120;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to fill-in the Category grid\n\n" +
                    ex.Message);
            }

        }

        //////////////////////////////////////////////////////////////////////
        // MySQL routines to run SELECTs and INSERT/UPDATE/DELETE.
        // The select returns a DataTable object. All routines use
        // A Dictionary <string, object> to pass parameterized 
        // quries. 
        //////////////////////////////////////////////////////////////////////
        #region MySQL routines

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string zGetKey(string key)
        {
            Dictionary<string, Object> parms = new Dictionary<string, object>();
            string sql = "";
            DataTable dt = null;
            string rtn = "";

            try
            {
                sql = "SELECT `Value` " +
                        "FROM `property` " +
                        "WHERE `Name`=@key;";
                parms.Add("@key", key);

                dt = zGetDataTable(sql, parms);
                if ((dt != null) && (dt.Rows.Count >= 0))
                {
                    DataRow row = dt.Rows[0];
                    rtn = row["Value"].ToString();
                }

                return rtn;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the property for: " +
                                    key + "\n\n" +
                                    ex.Message, "System Error");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private DataTable zGetDataTable(string sql, Dictionary<string, Object> parms)
        {
            DataTable dt = null;
            DataSet ds = null;

            //Trace.Enter(Trace.RtnName(mModName, "zGetDataTable"));

            try
            {
                ds = zGetDataSet(sql, parms);
                dt = ds.Tables[0];
                return dt;
            }

            catch (Exception ex)
            {
                throw ex;
                //EZException EZex = new EZException("Failed", ex);
                //EZex.Add("sql", sql);
                //EZex.Add("parms", parms);
                //throw EZex;
            }

            finally
            {
                //Trace.Exit(Trace.RtnName(mModName, "zGetDataTable"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        protected DataSet zGetDataSet(string sql, Dictionary<string, Object> parms)
        {
            //MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            //Trace.Enter(Trace.RtnName(mModName, "zGetDataSet"));

            try
            {
                cmd = new MySqlCommand(sql, mConn);
                if (parms != null)
                {
                    foreach (KeyValuePair<string, Object> parm in parms)
                    {
                        cmd.Parameters.AddWithValue(parm.Key, parm.Value);
                    }
                }

                da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                return ds;
            }

            catch (Exception ex)
            {
                //EZException EZex = new EZException("Failed", ex);
                //EZex.Add("sql", sql);
                //EZex.Add("parms", parms);
                throw ex;
            }

            finally
            {
                //Trace.Exit(Trace.RtnName(mModName, "zGetDataSet"));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        protected int zExecuteNonQuery(string sql, Dictionary<string, Object> parms)
        {
            //MySqlConnection conn = null;
            MySqlCommand cmd = null;
            int rtn = -1;

            try
            {
                //conn = new MySqlConnection(mConnectionString);
                //conn.Open();
                cmd = new MySqlCommand(sql, mConn);
                if (parms != null)
                {
                    foreach (KeyValuePair<string, Object> parm in parms)
                    {
                        cmd.Parameters.AddWithValue(parm.Key, parm.Value);
                    }
                }
                rtn = cmd.ExecuteNonQuery();

                try
                {
                    if (sql.Trim().ToLower().StartsWith("insert "))
                    {
                        mLastId = (int)cmd.LastInsertedId;
                    }
                }
                catch
                {
                    mLastId = -1;
                }

                return rtn;
            }

            catch (Exception ex)
            {
                //EZException EZex = new EZException("Failed", ex);
                //EZex.Add("sql", sql);
                //EZex.Add("parms", parms);
                throw ex;
            }

            finally
            {
                //Trace.Exit(Trace.RtnName(mModName, "zExecuteNonQuery"));
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////
        // Routines related to the Category grid.
        //////////////////////////////////////////////////////////////////
        #region Category

        /// <summary>
        /// 
        /// </summary>
        private void zFillCategories()
        {
            try
            {
                string sql = "SELECT * FROM category WHERE Active=1 Order By Seq;";
                DataTable dt = zGetDataTable(sql, null);
                dgvCatagories.DataSource = dt;
                dgvCatagories.Columns[0].Visible = false;
                dgvCatagories.Columns["Active"].Visible = false;
                dgvCatagories.Columns["Name"].Width = 100;
                dgvCatagories.Columns["Description"].Width = 200;
                dgvCatagories.Columns["Seq"].Width = 30;
                dgvCatagories.Columns["PageDescription"].Width = 390;

                tbCat.Text = dgvCatagories.Rows.Count.ToString();

                //Get the width of each column as a percentage of
                // the width of the grid at initial run time state
                // (same as design time).
                mCategoryWidths = new double[dgvCatagories.Columns.Count];
                for (int i = 0; i < dgvCatagories.Columns.Count; i++)
                {
                    mCategoryWidths[i] = ((double)dgvCatagories.Columns[i].Width) / 
                                            ((double)dgvCatagories.Width - 
                                                SystemInformation.VerticalScrollBarWidth);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to fill-in the Category grid\n\n" +
                    ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeCategoryColumns()
        {
            int i = 0;

            try
            {
                foreach (double d in mCategoryWidths)
                {
                    dgvCatagories.Columns[i++].Width =
                            Convert.ToInt32(d * ((double)dgvCatagories.Width -
                                            SystemInformation.VerticalScrollBarWidth));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to resize the Category grid columns\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// Routine to INSERT new Categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCategoryAdd_Click(object sender, EventArgs e)
        {
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                frmCategory frm = new frmCategory("Add", 0);
                
                frm.CatName = "";
                frm.CatDesc = "";
                frm.CatSeq = 5;
                frm.catPageDesc = "";

                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    sql = "INSERT INTO category(Name, Description, Seq, PageDescription) " +
                                "VALUES(@name, @desc, @seq, @pageDesc)";
                    parms.Add("@name", frm.CatName);
                    parms.Add("@desc", frm.CatDesc);
                    parms.Add("@seq", frm.CatSeq);
                    parms.Add("@pageDesc", frm.catPageDesc);

                    zExecuteNonQuery(sql, parms);
                    zFillCategories();
                }
                frm.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to update the Category\n\n" +
                    ex.Message);
            }
        }
        
        /// <summary>
        /// Mouse down need to capture the grid row we are
        /// in when the mouse down event occurs. The row 
        /// number is saved in mRecNo (all mouse downs
        /// on the various grids save in the same global
        /// variable).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCatagories_MouseDown(object sender, MouseEventArgs e)
        {
            zCatagories_MouseDown(e); 
        }

        /// <summary>
        /// Handle selected row change caused by mouse (left/right) click
        /// </summary>
        /// <param name="e"></param>
        private void zCatagories_MouseDown(MouseEventArgs e)
        {
            mRecNo = dgvCatagories.HitTest(e.X, e.Y).RowIndex;
            if (mRecNo >= 0)
            {
                dgvCatagories.Rows[mRecNo].Selected = true;
            }
        }

        /// <summary>
        /// Routine to UPDATE a category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCategoryEdit_Click(object sender, EventArgs e)
        {
            zCategoryEdit();
        }

        /// <summary>
        /// Handle a Category EDIT request. Needed because we can
        /// request edit by double click or by right click/edit.
        /// </summary>
        private void zCategoryEdit()
        {
            int recno = -1;
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvCatagories.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    frmCategory frm = new frmCategory("EDIT", recno);

                    frm.CatName = row.Cells["Name"].Value.ToString();
                    frm.CatDesc = row.Cells["Description"].Value.ToString();
                    frm.CatSeq = Convert.ToInt32(row.Cells["Seq"].Value.ToString());
                    frm.catPageDesc = row.Cells["PageDescription"].Value.ToString();

                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        sql = "UPDATE category SET " +
                                    "Name=@name, " +
                                    "Description=@desc, " +
                                    "Seq=@seq, " +
                                    "PageDescription=@pageDesc " +
                                "WHERE Id=@recno ";
                        parms.Add("@name", frm.CatName);
                        parms.Add("@desc", frm.CatDesc);
                        parms.Add("@seq", frm.CatSeq);
                        parms.Add("@pageDesc", frm.catPageDesc);
                        parms.Add("@recno", recno);
                        zExecuteNonQuery(sql, parms);
                        zFillCategories();
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the Category\n\n" +
                    "  Id: " + recno.ToString() + "\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// Double click in the grid will result in an EDIT of
        /// the row being double clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCatagories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            zCatagories_MouseDown(e);
            zCategoryEdit();
        }

        /// <summary>
        /// Routine to DELETE a category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCategoryDelete_Click(object sender, EventArgs e)
        {
            int recno = 0;
            
            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvCatagories.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    frmCategory frm = new frmCategory("DELETE", recno);

                    frm.CatName = row.Cells["Name"].Value.ToString();
                    frm.CatDesc = row.Cells["Description"].Value.ToString();
                    frm.CatSeq = Convert.ToInt32(row.Cells["Seq"].Value.ToString());
                    frm.catPageDesc = row.Cells["PageDescription"].Value.ToString();

                    frm.CatName = "name";

                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        //TODO: DELETE the category record?
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the Category\n\n" +
                    "  Id: " + recno.ToString() + "\n" +
                    ex.Message);
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////
        // 
        /////////////////////////////////////////////////////////////////////
        #region Vehicle

        /// <summary>
        /// 
        /// </summary>
        private void zFillVehicles()
        {
            try
            {
                string sql = "SELECT v.id, v.name, c.name AS Category, c.Id AS CategoryId," +
                                    "v.StartDate, v.EndDate, v.SmallPic, " +
                                    "v.LargePic, v.Featured, v.VIN, v.Price, " +
                                    "v.PageDescription, v.ShortDescription " +
                                "FROM vehicles AS v " +
                                    "JOIN category AS c ON(c.Id = v.CategoryID) " +
                                "WHERE v.Active = 1 " +
                                "Order By v.Id; ";
                DataTable dt = zGetDataTable(sql, null);
                dgvVehicles.DataSource = dt;
                dgvVehicles.Columns[0].Visible = false;
                dgvVehicles.Columns["CategoryId"].Visible = false;
                dgvVehicles.Columns["name"].Width = 75;
                dgvVehicles.Columns["Category"].Width = 75;
                dgvVehicles.Columns["StartDate"].Width = 75;
                dgvVehicles.Columns["EndDate"].Width = 75;
                dgvVehicles.Columns["SmallPic"].Width = 100;
                dgvVehicles.Columns["LargePic"].Width = 100;
                dgvVehicles.Columns["Featured"].Width = 50;
                dgvVehicles.Columns["VIN"].Width = 100;
                dgvVehicles.Columns["Price"].Width = 50;
                dgvVehicles.Columns["PageDescription"].Width = 300;
                dgvVehicles.Columns["ShortDescription"].Width = 300;

                tbVehicles.Text = dgvVehicles.Rows.Count.ToString();

                //Get the width of each column as a percentage of
                // the width of the grid at initial run time state
                // (same as design time).
                mVehicleWidths = new double[dgvVehicles.Columns.Count];
                for (int i = 0; i < dgvVehicles.Columns.Count; i++)
                {
                    mVehicleWidths[i] = ((double)dgvVehicles.Columns[i].Width) /
                                            ((double)dgvVehicles.Width -
                                                SystemInformation.VerticalScrollBarWidth);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to fill-in the Category grid\n\n" +
                    ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeVehicleColumns()
        {
            int i = 0;

            try
            {
                foreach (double d in mVehicleWidths)
                {
                    dgvVehicles.Columns[i++].Width =
                            Convert.ToInt32(d * ((double)dgvVehicles.Width -
                                            SystemInformation.VerticalScrollBarWidth));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to resize the Vehicle grid columns\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// ADD a Vehicle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int recno = -1;
            DataTable dt = null;
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvVehicles.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    dt = zGetCategoryDataTable();
                    frmVehicle frm = new frmVehicle("add", 0, dt);
                    
                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        sql = "INSERT INTO vehicles (name, StartDate, EndDate, SmallPic, " +
                                            "LargePic, CategoryId, Featured, VIN, Price, " +
                                            "PageDescription, ShortDescription) " +
                                "Values(@name, @StartDate, @EndDate, @SmallPic, @LargePic, " +
                                    "@CategoryId, @Featured, @VIN, @Price, " +
                                    "@PageDescription, @ShortDescription);";
                        parms.Add("@name", frm.VehName);
                        parms.Add("@StartDate", frm.VehStart);
                        parms.Add("@EndDate", frm.VehEnd);
                        parms.Add("@SmalPic", frm.VehSmallPic);
                        parms.Add("@LargePic", frm.VehLargePic);
                        parms.Add("@CategoryId", frm.VehCatId);
                        parms.Add("@Featured", frm.VehFeatured);
                        parms.Add("@VIN", frm.VehVIN);
                        parms.Add("@Price", frm.VehPrice);
                        parms.Add("@PageDescription", frm.VehPageText);
                        parms.Add("@ShortDescription", frm.VehDesc);
                        zExecuteNonQuery(sql, parms);
                        zFillVehicles();
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to add the Vehicle\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable zGetCategoryDataTable()
        {
            DataTable dt = null;
            string sql = "";

            try
            {
                sql = "SELECT `Id`, `Name` " +
                        "FROM `category` " +
                        "WHERE `Active`= 1 " +
                        "ORDER BY `Seq`; ";
                dt = zGetDataTable(sql, null);
                return dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to return category table.\n\n" +
                            ex.Message, "System Error");
                throw ex;
            }
        }

        /// <summary>
        /// Mouse click on the Vehicles grid. Sets the current row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVehicles_MouseDown(object sender, MouseEventArgs e)
        {
            zVehiclesMouseDown(e);
        }

        /// <summary>
        /// Handle selected row change caused by mouse (left/right) click
        /// </summary>
        /// <param name="e"></param>
        private void zVehiclesMouseDown(MouseEventArgs e)
        {
            mRecNo = dgvVehicles.HitTest(e.X, e.Y).RowIndex;
            if (mRecNo >= 0)
            {
                dgvVehicles.Rows[mRecNo].Selected = true;
            }
        }

        /// <summary>
        /// EDIT a vehicle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            zEditVehicle();
        }

        /// <summary>
        /// Handle a Category EDIT request. Needed because we can
        /// request edit by double click or by right click/edit.
        /// </summary>
        private void zEditVehicle()
        {
            int recno = -1;
            DataTable dt = null;
            string value = "";
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvVehicles.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    dt = zGetCategoryDataTable();
                    frmVehicle frm = new frmVehicle("edit", recno, dt);

                    frm.VehName = row.Cells["name"].Value.ToString();
                    frm.VehCatId = Convert.ToInt32(row.Cells["CategoryId"].Value.ToString());
                    frm.VehVIN = row.Cells["VIN"].Value.ToString();

                    value = row.Cells["Price"].Value.ToString();
                    if (value.Trim().Length == 0) { value = "0.00"; }
                    frm.VehPrice = Convert.ToDouble(value);

                    frm.VehStart = Convert.ToDateTime(row.Cells["StartDate"].Value.ToString());

                    value = row.Cells["EndDate"].Value.ToString();
                    if (value.Trim().Length == 0)
                    {
                        frm.VehEnd = DateTime.MaxValue;
                    }
                    else
                    {
                        frm.VehEnd = Convert.ToDateTime(value);
                    }

                    sql = "SELECT `Id`, `GallaryPic` " +
                            "FROM `gallary` " +
                            "WHERE `VehicleId`= @VehId; ";
                    parms = new Dictionary<string, object>();
                    parms.Add("@VehId", recno);
                    frm.GallaryPicTable = zGetDataTable(sql, parms);

                    frm.VehSmallPic = row.Cells["SmallPic"].Value.ToString();
                    frm.VehLargePic = row.Cells["LargePic"].Value.ToString();
                    frm.VehDesc = row.Cells["ShortDescription"].Value.ToString();
                    frm.VehPageText = row.Cells["PageDescription"].Value.ToString();

                    frm.StagingPath = Settings1.Default.StagingPath;
                    frm.SmallPicPath = Settings1.Default.LocalSmallPicPath; //zGetKey("LocalSmallPicPath");
                    frm.LargePicPath = Settings1.Default.LocalLargePicPath; //zGetKey("LocalLargePicPath");
                    frm.GallaryPath = Settings1.Default.LocalGallaryPicPath; //zGetKey("LocalGallaryPicPath");

                    frm.VehFeatured = Convert.ToInt32(row.Cells["Featured"].Value.ToString());
                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        sql = " UPDATE vehicles SET " +
                                        "name = @name, " +
                                        "StartDate = @StartDate, " +
                                        "EndDate = @EndDate, " +
                                        "SmallPic = @SmallPic, " +
                                        "LargePic = @LargePic, " +
                                        "CategoryId = @CategoryId, " +
                                        "Featured = @Featured, " +
                                        "VIN = @VIN, " +
                                        "Price = @Price, " +
                                        "PageDescription = @PageDescription, " +
                                        "ShortDescription = @ShortDescription " +
                                    "WHERE id = @recno; ";
                        parms.Add("@name", frm.VehName);
                        parms.Add("@StartDate", frm.VehStart);
                        parms.Add("@EndDate", frm.VehEnd);
                        parms.Add("@SmallPic", frm.VehSmallPic);
                        parms.Add("@LargePic", frm.VehLargePic);
                        parms.Add("@CategoryId", frm.VehCatId);
                        parms.Add("@Featured", frm.VehFeatured);
                        parms.Add("@VIN", frm.VehVIN);
                        parms.Add("@Price", frm.VehPrice);
                        parms.Add("@PageDescription", frm.VehPageText);
                        parms.Add("@ShortDescription", frm.VehDesc);
                        parms.Add("@recno", recno);

                        zExecuteNonQuery(sql, parms);
                        zFillVehicles();
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to update the Vehicle\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// Double click on a row will result in an
        /// EDIT operation for that row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVehicles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            zVehiclesMouseDown(e);
            zEditVehicle();
        }

        /// <summary>
        /// DELETE a vehicle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int recno = 0;
            DataTable dt = null;
            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvVehicles.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    dt = zGetCategoryDataTable();
                    frmVehicle frm = new frmVehicle("delete", recno, dt);
                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        //TODO: DELETE the vehicle record?
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the Category\n\n" +
                    "  Id: " + recno.ToString() + "\n" +
                    ex.Message);
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////
        //
        ////////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// This routine will INSERT a new Property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPropertiesAdd_Click(object sender, EventArgs e)
        {
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                frmProperty frm = new frmProperty("add", 0);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    sql = "SELECT * " +
                            "FROM property " +
                            "WHERE Name = @name ";
                    parms.Add("@name", frm.PropName);
                    DataTable dt = zGetDataTable(sql, parms);
                    if ((dt == null) || (dt.Rows.Count == 0))
                    {
                        sql = "INSERT INTO property (Name, Value, Description) " +
                                "VALUES(@name, @value, @desc); ";
                        parms = new Dictionary<string, object>();
                        parms.Add("@name", frm.PropName);
                        parms.Add("@value", frm.PropValue);
                        parms.Add("@desc", frm.PropDesc);

                        zExecuteNonQuery(sql, parms);
                        zFillProperties();
                    }
                    frm.Close();
                }
            }

            catch (Exception ex) 
            {
                MessageBox.Show("Failed to add the Property\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// Set mRecNo to the row the mouse was on when it was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProperties_MouseDown(object sender, MouseEventArgs e)
        {
            mRecNo = dgvProperties.HitTest(e.X, e.Y).RowIndex;
            dgvProperties.Rows[mRecNo].Selected = true;
        }

        /// <summary>
        /// This routine will UPDATE a Property.
        /// <summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPropertiesEdit_Click(object sender, EventArgs e)
        {
            int recno = 0;
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvCatagories.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    frmProperty frm = new frmProperty("edit", recno);
                    
                    frm.PropName = row.Cells["name"].Value.ToString();
                    frm.PropValue = row.Cells["value"].Value.ToString();
                    frm.PropDesc = row.Cells["description"].Value.ToString();

                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        sql = "UPDATE property SET " +
                                    "Name = @name, " +
                                    "Value = @value, " +
                                    "Description = @desc " +
                                "WHERE Id = @recno;";
                        parms.Add("@name", frm.PropName);
                        parms.Add("@value", frm.PropValue);
                        parms.Add("@desc", frm.PropDesc);
                        parms.Add("@recno", recno);

                        zExecuteNonQuery(sql, parms);
                        zFillProperties();
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to update the Property\n\n" +
                    ex.Message);
            }
        }

        /// <summary>
        /// Routine to DELETE a Property record. 
        /// NOTE: Not sure I'm going to allow this.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPropertiesDelete_Click(object sender, EventArgs e)
        {
            int recno = 0;
            string sql = "";
            Dictionary<string, object> parms = new Dictionary<string, object>();

            try
            {
                if (mRecNo >= 0)
                {
                    DataGridViewRow row = dgvCatagories.Rows[mRecNo];
                    string cellData = row.Cells["Id"].Value.ToString();
                    recno = Convert.ToInt32(cellData);
                    frmProperty frm = new frmProperty("delete", recno);
                    
                    frm.PropName = row.Cells["name"].Value.ToString();
                    frm.PropValue = row.Cells["value"].Value.ToString();
                    frm.PropDesc = row.Cells["description"].Value.ToString();

                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        //TODO: DELETE the property record?
                    }
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the Property\n\n" +
                    ex.Message);
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////
        // Settings
        ////////////////////////////////////////////////////////////////////
        #region Settings

        /// <summary>
        /// Fill-in the fields of the Settings Tab Page
        /// </summary>
        private void zFillSettings()
        {
            tbMySQLServer.Text = Settings1.Default.server;
            tbMySQLDatabase.Text = Settings1.Default.db;
            tbMySQLUserName.Text = Settings1.Default.uid;
            tbMySQLPassword.Text = Settings1.Default.password;

            psRootPath.dirPath = Settings1.Default.StagingPath;
            psSmallPicPath.dirPath = Settings1.Default.LocalSmallPicPath;
            psLargePicPath.dirPath = Settings1.Default.LocalLargePicPath;
            psGallaryPath.dirPath = Settings1.Default.LocalGallaryPicPath;

            btnSave.BackColor = SystemColors.Window;
        }

        private void psSmallPicPath_Enter(object sender, EventArgs e)
        {
            string spath;
            spath = psRootPath.dirPath.Replace("\\", "/");
            spath = spath.Replace("//", "/");
            ((PathSearch)sender).RootPath = spath;
        }

        /// <summary>
        /// The user wants to save any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings1.Default.server = tbMySQLServer.Text.Trim();
            Settings1.Default.db = tbMySQLDatabase.Text.Trim();
            Settings1.Default.uid = tbMySQLUserName.Text.Trim();
            Settings1.Default.password = tbMySQLPassword.Text.Trim();

            Settings1.Default.StagingPath = psRootPath.dirPath;
            Settings1.Default.LocalSmallPicPath = psSmallPicPath.dirPath;
            Settings1.Default.LocalLargePicPath = psLargePicPath.dirPath;
            Settings1.Default.LocalGallaryPicPath = psGallaryPath.dirPath;

            Settings1.Default.Save();
            btnSave.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMySQLServer_TextChanged(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.Red;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void psRootPath_Leave(object sender, EventArgs e)
        {
            if (((PathSearch)sender).Changed)
            {
                btnSave.BackColor = Color.Red;
            }
        }

        #endregion

    }
}
