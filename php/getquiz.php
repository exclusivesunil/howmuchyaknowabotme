<?php

include 'config.php';
include 'function.php';



$erroInsert = false;
// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$random_hash =$_GET['random_hash'];


$sql ="SELECT * FROM quizes  where  random_hash= '".$random_hash ."'";
//$sql ="SELECT * FROM quizes, questions, options where quizes.quizId = questions.quizid and options.questionId = questions.questionId and random_hash= '".$random_hash ."'";
$result= mysqli_query($conn,$sql);
$returnval=[];

$quizArray = mysqli_fetch_all($result, MYSQLI_ASSOC);
foreach ($quizArray as $quiz) {
    $quizId = $quiz['quizId'];
    $returnval['id'] = $quiz['quizId'];
}


$sql ="SELECT * FROM questions where  quizId= '".$quizId ."'";
$result= mysqli_query($conn,$sql);
$questionArray = mysqli_fetch_all($result, MYSQLI_ASSOC);

for ($i=0;$i<count($questionArray); $i++) {

$sql ="SELECT * FROM options where   questionId= '".$questionArray[$i]['questionId'] ."'";
$result= mysqli_query($conn,$sql);
$optionArray = mysqli_fetch_all($result, MYSQLI_ASSOC);
$questionArray[$i]['options'] =$optionArray;

}

$returnval['questions'] = $questionArray;

echo (json_encode($returnval));



exit;

var_dump($row);

    exit();


while ($row = $result->fetch_assoc())  
{
    echo $row["quiz_name"] ;
}



if($erroInsert == false)
{
  $returnArry=array("success"=>"1" ,"random_hash" => $random_hash);
}
else
{
  $returnArry=array("success"=>"0");

}

echo json_encode($returnArry);

?>