<?php

require_once("DB_test_unity.php");
$pdo=connectDB();


//POST受け取り
if(isset($_POST["id"])){
	$id=$_POST["id"];//要求されてくるID
}

if(isset($_POST["pass"])){
	$pass=$_POST["pass"];//要求されてくるPass
}

//入力されたIDが0、マイナス値、文字、であればエラー値として9999代入
if(!is_numeric($id)||$id>=9999||$id<=0){
	$id=9999;
}



try{

$stmt=$pdo->query("SELECT * FROM player_data WHERE id=".$id);


	if($id !=9999){
	
	foreach($stmt as $row){
		if($row['pass']==$pass){
		$res="Player ";
		$res=$res.$row['name'];
	}else{
	$res="Pass Error";
	}
	}
	/*
	foreach($stmt as $row){
		$res=$row['id'];
		$res=$res.$row['name'];
		$res=$res.$row['pass'];
		$res=$res.$row['play_minute'];
		$res=$res.$row['play_second'];
		$res=$res.$row['score'];
		}*/
/*	}else{
	$res="Pass Error";
	}*/
	}
	else{
	$res="Error";	//入力されたIDがエラー値の場合
	}

	

} catch (PDOException $e){
	//$id=9999;
	var_dump($e->getMessage());
}
$pdo=null;	//DB切断

if(empty($res)){
	$res=null;	//入力されたIDがなければError
}

echo $res;	//Unityに結果を返す

?>
