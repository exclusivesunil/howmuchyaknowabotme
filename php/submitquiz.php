<?php

include 'config.php';

$quiz ='{
  "quizName": "Quiz for my love",
  "options": [
    {
      "questionName": "test",
      "option1": "new",
      "option2": "now",
      "option3": "work",
      "option4": "owo",
      "answer": "4"
    },
    {
      "questionName": "color",
      "option1": "3",
      "option2": "i",
      "option3": "8",
      "option4": "44",
      "answer": "1"
    }
  ]
}';


$servername = "localhost";
$username = "root";
$password = "sunil123";
$dbname = "carvemylove";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}



$quizJson= (json_decode($quiz));

function guidv4($data)
{
    assert(strlen($data) == 16);

    $data[6] = chr(ord($data[6]) & 0x0f | 0x40); // set version to 0100
    $data[8] = chr(ord($data[8]) & 0x3f | 0x80); // set bits 6-7 to 10

    return vsprintf('%s%s-%s-%s-%s-%s%s%s', str_split(bin2hex($data), 4));
}

$random_hash = guidv4(openssl_random_pseudo_bytes(16));

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 




for ($x = 0; $x <= count($quizJson); $x++) {
    var_dump($quizJson);

   // $sqlQuesiton ="insert into quizes (quiz_name, random_hash, answer) values('".$quizJson->answer[$x]->questionName ."', '".$random_hash ."',".$quizJson->answer[$x]->answer .")";
      
    $sqlQuesiton ="insert into quizes (quiz_name, random_hash) values('".$quizJson->quizName ."', '".$random_hash ."')";

    echo $sqlQuesiton."<Br>";


} 
?>