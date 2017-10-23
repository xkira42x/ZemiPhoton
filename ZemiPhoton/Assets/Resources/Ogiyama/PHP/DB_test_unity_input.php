<?php

require_once("DB_test_unity.php");
$pdo=connectDB();

	//$name=$_POST["name"];	//入力されたPass

if(isset($_POST["name"])){
	$name=$_POST["name"];	
}

try{

if(!empty($name)){
if(!$stmt=$pdo->query("insert into player_data (id,name) select max(id)+1,'$name' from player_data")){
$res=null;
}
}else{
$res=null;
}



} catch (PDOException $e){
	var_dump($e->getMessage());
}
$pdo=null;	//DB切断

echo $res;

?>
