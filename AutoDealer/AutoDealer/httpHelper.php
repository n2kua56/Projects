<?php

/**
 * httpHelper short summary.
 *
 * httpHelper description.
 *
 * @version 1.0
 * @author David
 */
class httpHelper
{
    ///<summary>
    /// Builds the http header objects:
    ///   1 <!doctype
    ///   2 <html
    ///   3 <head
    ///   4 <meta statements
    ///   5 <title (if specified)
    ///   6 <link statements (if specified)
    ///   7 <script statements (if specified)
    ///</summary>
    public function httpHead($title, $arrayMeta, $arrayLink, $arrayScript)
    {
        ////////////////////////////////////
        //First elements of an HTML document
        $head = '<!doctype html>' . "\n" .
                '<html lang="en" style="margin: 5px">' . "\n" .
                '  <head>' . "\n" .
                '    <meta charset="utf-8">' . "\n";

        /////////////////////////////
        //Title if one was specified.
        if (strlen($title) != 0)
        {
            $head .= "    <title>" . $title . "</title>\n";
        }

        ////////////////////////////////
        //META data if any was specified
        if (is_array($arrayMeta))
        {
            $head .= "\n";
            if (array_key_exists("author" , $arrayMeta ))
            {
                $head .= '    <meta name="author" content="' .
                    $arrayMeta['author'] . "\">\n";
            }
            if (array_key_exists ("description" , $arrayMeta ))
            {
                $head .= '    <meta name="description" content="' .
                    $arrayMeta['description'] . "\">\n";
            }
            if (array_key_exists ("keywords" , $arrayMeta ))
            {
                $head .= '    <meta name="keywords" content="' .
                    $arrayMeta['keywords'] . "\">\n";
            }
            if (array_key_exists ("application-name" , $arrayMeta ))
            {
                $head .= '    <meta name="application-name" content="' .
                    $arrayMeta['application-name'] . "\">\n";
            }
        }

        ///////////////////////////////////
        //LINK statements if any specified,
        if (is_array($arrayLink))
        {
            $head .= "\n";
            foreach ($arrayLink as $value)
            {
                $head .= '    <link rel="stylesheet" type="text/css" href="' . $value . '">' . "\n";
            }
        }

        /////////////////////////////////////
        //SCRIPT statements if any specified.
        if (is_array($arrayScript))
        {
            $head .= "\n";
            foreach ($arrayScript as $value)
            {
                $head .= '    <script type="text/javascript" src="' . $value . '"></script>' . "\n";
            }
        }

        /////////////////////////////////////////////
        //There needs to be a parameter for the logo.
        $head .= '    <link rel="icon" href="images\Cars-R-Us-logo.png">' . "\n";
        $head .= '  </head>' . "\n";

        return $head;
    }

    ///<summary>
    /// Adds the <body tag and <h1 
    ///</summary>
	public function httpBody($heading, $scripts)
	{
		$body = "\n  <body>\n";
		if (strlen($heading) > 0)
		{
			$body .= "    <h1>" . $heading . "</h1>\n";
		}
		return $body;
	}

    ///<summary>
    ///This adds the standard icon/text
    ///</summary>
    function SitePageHeading()
    {
        $siteheading = "";

        $siteheading .= '    <div id="myheader" width="100%">' . "\n";
        $siteheading .= '      <div style=float:left; id="siteicon" align="left" width="50%">' . "\n";
        $siteheading .= '        <img src="images\Cars-R-Us-logo.png" alt="Cars-R-Us" height="180" width="180">' . "\n";
        $siteheading .= '      </div>' . "\n";
        $siteheading .= '      <div  style="float:right; id="sitetitle"; align: right; width=50%;">' . "\n";
        $siteheading .= '        <p style="font-size: 40px; text-align: center;">Cars-R-Us</p>' . "\n";
        $siteheading .= '      </div>' . "\n";
        $siteheading .= '    </div>' . "\n";
        $siteheading .= '    <div style="clear: both"></div>' . "\n";

        return $siteheading;
    }

    //This recursive routine processes a menu array.
    //  $arrayMenu : The key/value pair menu array (doesn't need to pass)
    //  $currentSet: 1 if current already set (not sure this will pass back)
    //  $indent    : The number of spaces to indent (doesn't matter if it passes)
    function zMenuArray($arrayMenu, $currentSet, $indent)
    {
        $menu = "";
        foreach ($arrayMenu as $key => $value)
        {
            //Start the List Item.
            $menu .= str_repeat(" ", $indent);
            $menu .= '<li';

            //Do we need to set the "current-menu-item"?
            if ($currentSet == 0)
            {
                $menu .= ' class="current-menu-item"';
                $currentSet = 1;
            }

            //Does this item have a sub-menu?
            if (is_array($value))
            {
                $menu .= ' class="parent">';
                $menu .= '<a href="' . $value[0] . '">' . $key . '</a>' . "\n";
                $indent += 2;
                $menu .= str_repeat(" ", $indent) . '<ul class="sub-menu">' . "\n";

                $menu .= $this->zMenuArray($value[1], $currentSet, $indent);

                $menu .= str_repeat(" ", $indent) . "</ul>\n";
                $indent -= 2;
                $menu .= str_repeat(' ', $indent) . '</li>' . "\n";
            }
            else
            {
                $menu .= '>';
                $menu .= '<a href="' . $value . '">' . $key . '</a></li>' . "\n";
            }
        }

        return $menu;
    }

    /// <summary>
    /// </summary>
    /// <input $menuId>The id tag for the first ul item.</input>
    /// <input $arrayMenu>The
    public function httpMenu($menuId, $arrayMenu)
    {
        //TODO: Make sure the menu is inside the horizontal bar.
        $currentSet = 0;
        $indent = 14;

        $menu = "\n" . '<div class="clear"></div>' . "\n";
        $menu .= '    <div id="wrap">' . "\n";
        $menu .= '      <header>' . "\n";
        $menu .= '        <div class="inner relative">' . "\n";
        $menu .= '          <nav id="navigation">' . "\n";
        $menu .= '            <ul id="' . $menuId . '">' . "\n";

        $menu .= $this->zMenuArray($arrayMenu, $currentSet, $indent);

        $menu .= '            </ul>' . "\n";
        $menu .= '          </nav>' . "\n";
        $menu .= '          <div class="clear"></div>' . "\n";
        $menu .= '        </div>' . "\n";
        $menu .= '      </header>' . "\n";
        $menu .= '    </div>' . "\n";

        return $menu;
    }

    ///
	public function httpEndBody()
	{
        //TODO: Add the footer that will also have copyright.
        $html = '    </div>' . "\n";
		$html .= '  </body>' . "\n";
        $html .= '</html>' . "\n";
		return $html;
	}
//<body>
//  <script src="js/scripts.js"></script>
//</body>
//</html>
}