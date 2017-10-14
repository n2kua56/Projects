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

    //START OF THE MAIN PAGE
    echo '    <div style="width: 100%">' . "\n";

    echo '      <h1>HOME</h1>' . "\n";
    echo '      <p style="text-align: center; font-size: 16px;">Cars-R-Us<br>' . "\n";
    echo '1170 Front Street<br>' . "\n";
    echo 'Binghamton, NY 13905</p>' . "\n";
    echo '      <p style="text-align: center;">(607)795-4000</p>' . "\n";
    echo '      <p style="text-align: center;">Eddie Rouhana</p>' . "\n";

    //TODO: Featured vehicles.

    //TEST OF SQL
    $conn = new mysqli($hn, $un, $pw, $db);
    if ($conn->connect_error) die($conn->connect_error);

    $query = "SELECT * FROM `category`;";
    $results = $conn->query($query);
    if (!$results) die($conn->error);

    echo '   <h2>SQL TEST</h2>' . "\n";
    $rows = $results->num_rows;
    for ($j=0; $j<$rows; ++$j)
    {
        $results->data_seek($j);
        echo '    Category: ' . $results->fetch_assoc()['Name'] . '<br>' . "\n";
    }

    $results->close();
    $conn->close();

	echo $httphelper->httpEndBody();

    function mysql_error_msg($msg)
    {
        $msg2 = mysql_error();
        echo <<< _END
We are sorry, but it was not possible to complete
the requested task. The error message was:

    <p>$msg: $msg2</p

Please click the back button on your browser
and try again. If you are still having problems,
please <a href="mailto:admin@autodealer.davidearlwheeler.com">email
our administrator</a>. Thank you.
_END;
    };
?>