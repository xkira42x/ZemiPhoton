<?php

function connectDB(){

$db='mysql:dbname=trydb1;host=localhost';
$user='root';
$password='yda474GAKUSEI';
	try{
	$pdo = new PDO($db, $user, $password);
	}catch(PDOException $e){
	exit('' . $e->getMessage());
	}
	return $pdo;
}

?>


