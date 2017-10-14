<?php
require_once 'login.php';
require_once 'httpHelper.php';

$httphelper = new httpHelper();

$arrayMeta = array(
    'description' => 'Cars-R-Us Home page',
    'author' => 'David Wheeler',
    'application-name' => 'Cars-R-Us');
$arrayLink = array(
    "css/font-awesome.css",
    "css/menu.css");
$arrayScript = array(
    "js/jquery.js",
    "js/function.js");
$head = $httphelper->httpHead("Cars-R-Us Home", $arrayMeta, $arrayLink, $arrayScript);
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

//START OF THE INVENTORY PAGE
echo '    <div style="width: 100%">' . "\n";

echo '      <h1>INVENTORY</h1>' . "\n";

echo '    </div>' . "\n";

//$results->close();
//$conn->close();

echo $httphelper->httpEndBody();

?>