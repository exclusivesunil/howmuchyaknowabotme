<?php

include 'config.php';
include 'function.php';

$quiz ='{"options":[{"questionName":"now","option1":"ii","option2":"oo","option3":"99","option4":"88","answer":"1"},{"questionName":"question1","option1":"now","option2":"now2","option3":"now3","option4":"now4","answer":""},{"questionName":"question1","option1":"now","option2":"now2","option3":"now3","option4":"now4","answer":""}]}
';


$erroInsert = false;
// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$quizJson= json_decode((file_get_contents('php://input')));



$random_hash = guidv4(openssl_random_pseudo_bytes(16));
$sqlQuiz ="insert into quizes ( random_hash) values('".$random_hash ."')";
if (mysqli_query($conn, $sqlQuiz)) {
    $quizId = mysqli_insert_id($conn);
} else {
  echo $error;
  $erroInsert = true;
}



for ($x = 0; $x <= count($quizJson); $x++) {

$answerArray = array(false, false, false, false );
$sqlQuestion ="insert into questions (name,quizId) values('".$quizJson->options[$x]->questionName ."', ".$quizId.")";
$answerArray[((int)$quizJson->options[$x]->answer-1)] = true;

if (mysqli_query($conn, $sqlQuestion)) {
    $questionId = mysqli_insert_id($conn);
} else {
    $erroInsert = true;
}


$sqlOption1 ="insert into options (name,questionid, isAnswer) values('".$quizJson->options[$x]->option1 ."', ".$questionId.", ".(int)$answerArray[0].")";

if (!mysqli_query($conn, $sqlOption1)) {
  $erroInsert = true;
}

$sqlOption2 ="insert into options (name,questionid, isAnswer) values('".$quizJson->options[$x]->option2 ."', ".$questionId.", ".(int)$answerArray[1].")";

if (!mysqli_query($conn, $sqlOption2)) {
  $erroInsert = true;
}

$sqlOption3 ="insert into options (name,questionid, isAnswer) values('".$quizJson->options[$x]->option3 ."', ".$questionId.", ".(int)$answerArray[2].")";

if (!mysqli_query($conn, $sqlOption3)) {
 
  $erroInsert = true;
}

$sqlOption4 ="insert into options (name,questionid, isAnswer) values('".$quizJson->options[$x]->option4 ."', ".$questionId.", ".(int)$answerArray[3].")";

if (!mysqli_query($conn, $sqlOption4)) {
 
  $erroInsert = true;
}

}



if($erroInsert == false)
{
  $returnArry=array("success"=>"1", 'random_hash'=> $random_hash);
}
else
{
  $returnArry=array("success"=>"0");

}

echo json_encode($returnArry);

?>