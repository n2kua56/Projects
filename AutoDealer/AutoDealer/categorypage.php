<?php
//TODO: Parameritized qurries.

require_once 'login.php';
require_once 'httpHelper.php';

$pagetype = $_GET["type"];
switch ($pagetype)
{
    case "awd4wd":
        $pageTitle = "All Wheel Drive & 4 Wheel Drive";
        break;
    case "convertible":
        $pageTitle = "Convertibles";
        break;
    case "coupe":
        $pageTitle = "Coupe";
        break;
    case "luxury":
        $pageTitle = "Luxury";
        break;
    case "sedan":
        $pageTitle = "Sedans";
        break;
    case "suvcrossover":
        $pageTitle = "SUV & Crossovers";
        break;
    case "trucks":
        $pageTitle = "Trucks";
        break;
    default:
}
$httphelper = new httpHelper();

//TODO: Set the margin around the page.
$arrayMeta = array(
    'description' => 'Cars-R-Us ' . $pageTitle . 'page',
    'author' => 'David Wheeler',
    'application-name' => 'Cars-R-Us');
$arrayLink = array(
    "css/font-awesome.css",
    "css/menu.css",
    "css/autodealer.css");
$arrayScript = array(
    "js/jquery.js",
    "js/function.js");
$head = $httphelper->httpHead("Cars-R-Us " . $pageTitle, $arrayMeta, $arrayLink, $arrayScript);
echo $head;

echo $httphelper->httpBody("", "");

//Header section of the web page
echo $httphelper->SitePageHeading();

$menu = array(
    'main' => 'index.php',
    'inventory' => array('inventory.php',
        array(
            'AWD-4WD' => 'categorypage.php?type=awd4wd',
            'Convertible' => 'categorypage.php?type=convertible',
            'Coupe' => 'categorypage.php?type=coupe',
            'Luxury' => 'categorypage.php?type=luxury',
            'Sedan' => 'categorypage.php?type=sedan',
            'SUV-Crossover' => 'categorypage.php?type=suvcrossover',
            'Trucks' => 'categorypage.php?type=trucks')
        ),
    'Contact Us' => 'contactus.php');
echo $httphelper->httpMenu("main-menu", $menu);

////////////////////////////
//START OF THE CATEGORY PAGE
echo '    <div style="width: 100%">' . "\n";
echo '      <h1>' . $pageTitle . '</h1>' . "\n";

$conn = new mysqli($hn, $un, $pw, $db);
if ($conn->connect_error) die($conn->connect_error);

//Get the small pic path
$query = 'SELECT `Value` ' .
	        'FROM `property` ' .
            "WHERE `Name` = 'SmallPicPath'";
$results = $conn->query($query);
if (!$results) die($conn->error);
$rows = $results->num_rows;
for ($j=0; $j<$rows; ++$j)
{
    $results->data_seek($j);
    $smallPicPath = $results->fetch_assoc()['Value'];
    echo 'Small Pic Path: ' . $smallPicPath . '<br>' . "\n";
}
$results->close();

//Get the large pic path
$query = 'SELECT `Value` ' .
	        'FROM `property` ' .
            "WHERE `Name` = 'LargePicPath'";
$results = $conn->query($query);
if (!$results) die($conn->error);
$rows = $results->num_rows;
for ($j=0; $j<$rows; ++$j)
{
    $results->data_seek($j);
    $largePicPath = $results->fetch_assoc()['Value'];
    echo 'Large Pic Path: ' . $largePicPath . '<br>' . "\n";
}
$results->close();

//Get the small pic and large pic file names for each vehicle in the category
$query = 'SELECT `SmallPic`, `LargePic` ' .
	        'FROM `category` AS c ' .
                'JOIN `vehicles` AS v ON(c.Id = v.CategoryId) ' .
            "WHERE c.name = '" . $pagetype . "' " .
                "AND ('2016-08-31' > v.StartDate OR v.StartDate IS NULL) " .
                "AND ('2016-08-31' <= v.EndDate OR v.EndDate IS NULL)";
$results = $conn->query($query);
if (!$results) die($conn->error);

echo 'Vehicles in ' . $pageTitle . '.<br>' . "\n";
$rows = $results->num_rows;
for ($j=0; $j<$rows; ++$j)
{
    $results->data_seek($j);
    $vehicleSmallPic = $results->fetch_assoc()['SmallPic'];

    echo '<div class="floating-box" width="300px" height="170px">';
    echo '<p style="float: left;">' .
            '<img src="images/' . $smallPicPath . '/' . $vehicleSmallPic .
            '" height="150" width="200"></p>';
    echo '<p>Descriptive text</p>' . "\n";
    echo '</div>';

    //TODO: Large pic value is not coming in right
    //TODO: Need the description of the vehicle

    $vehicleLargePic = $results->fetch_assoc()['LargePic'];
    //echo '  Small Picture file: ' . $vehicleSmallPic . '<br>' . "\n";
    //echo '  Large Picture File: ' . $vehicleLargePic . '<br><p></p>' . "\n";
}
$results->close();
echo '<div style="clear: both"></div>' . "\n";
//END OF THE CATEGORY PAGE
//////////////////////////
echo '    </div>' . "\n";

//$results->close();
//$conn->close();

echo $httphelper->httpEndBody();
?>