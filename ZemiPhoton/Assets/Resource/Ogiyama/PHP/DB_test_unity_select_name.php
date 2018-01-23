<?php

require_once("DB_test_unity.php");
$pdo=connectDB();


//POST受け取り
if(isset($_POST["name"])){
	$name=$_POST["name"];//要求されてくるID
}


try{
//テーブル内にあるポストされた名前を抽出
$stmt=$pdo->query("SELECT*FROM player_data WHERE name='$name'");

	foreach($stmt as $row){
		//ポストされたnameがテーブル内にあれば$resに「Log In!」代入
		if($row['name']==$name){
		$res="Log In!";
		}
	}
	//$resに代入された値がempty（無）ならば、$resに「Not Name」代入
	if(empty($res)){
	$res="Not Name";
	}

} catch (PDOException $e){
	//$id=9999;
	var_dump($e->getMessage());
}
$pdo=null;	//DB切断
/*
if(empty($res)){
	$res="Not ID.";	//入力されたIDがなければError
}*/

echo $res;	//Unityに結果を返す

?>
