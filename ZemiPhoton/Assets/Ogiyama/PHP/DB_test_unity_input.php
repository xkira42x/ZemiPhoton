<?php

require_once("DB_test_unity.php");
$pdo=connectDB();


	$pass=$_POST["pass"];	//入力されたPass


if(isset($_POST["name"])){
	$name=$_POST["name"];
}


try{

$stmt=$pdo->query();

} catch (PDOException $e){
	var_dump($e->getMessage());
}
$pdo=null;	//DB切断

if(empty($res)){
	$res=null;	//入力されたPassがなければError
}

echo $res;	//Unityに結果を返す

?>
