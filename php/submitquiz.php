<?php

$quiz ='{
  "answer": [
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


$quizJson= (json_decode($quiz));

function guidv4($data)
{
    assert(strlen($data) == 16);

    $data[6] = chr(ord($data[6]) & 0x0f | 0x40); // set version to 0100
    $data[8] = chr(ord($data[8]) & 0x3f | 0x80); // set bits 6-7 to 10

    return vsprintf('%s%s-%s-%s-%s-%s%s%s', str_split(bin2hex($data), 4));
}

$random_hash = guidv4(openssl_random_pseudo_bytes(16));


for ($x = 0; $x <= count($quizJson); $x++) {
    var_dump($quizJson->answer[$x]);

    $sqlQuesiton ="insert into quizes (quiz_name, random_hash, answer) values('".$quizJson->answer[$x]->questionName ."', '".$random_hash ."',".$quizJson->answer[$x]->answer .")";
    echo $sqlQuesiton."<Br>";
} 
?>