using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Helper = Neo.SmartContract.Framework.Helper;
using System.Text;

using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;


/*
 * @Introduction
 * 'How much you know about me' is a NEO-based blockchain game with following flow:
 * 1. The girl goes to our website to create some quiz, a invitation URL would be generated after submission. 
 * 2. The girl sends the invitation URL to the boy. The boy must correctly answer all the quiz correctly in order to get a chance to post a message to the girl onto blockchain.
 * 3. The message includes all the Q&A in this game, a long with the boy's message to her. It will stay on blockchain eternally for their love.
 * 
 */

namespace Howmuch
{
	public class Howmuch: SmartContract
    {

        [Serializable]
		public class Question{
			public byte[] title;    //The title of a question
			public byte[] answer;   //The correct answer of this quesion. Only the correct answr would be recorded onto blockchain.
		}

        [Serializable]
		public class Quiz{
			public byte[] nameInquirer;     //The name of the quiz inquirer   
			public byte[] nameAnswerer;     //The name of the quiz answerer
			public byte[] message;          //The message that answerer gives to the inquierer
			public BigInteger[] questionIDs;  //The IDs of all questions. Require special (de)serialization methods

		}

		//TBD: (De)serialization implementation of questions
		public static byte[] Serialize(Question question){
			return new byte[10];

		}

		//TBD: (De)serialization implementation of questions
		public static Question DeserializeToQuestion(byte[] data){
			Question question = new Question();
			question.title = "1010101".HexToBytes();
			return question;
		}

		//TBD: (De)serialization implementation of quiz
		public static byte[] Serialize(Quiz quiz)
        {
            return new byte[10];

        }

		//TBD: (De)serialization implementation of quiz
		public static Quiz DeserializeToQuiz(byte[] data)
        {
			Quiz quiz = new Quiz();
			quiz.nameAnswerer = "abcde".HexToBytes();
			quiz.nameInquirer = "edcba".HexToBytes();
			return quiz;
        }

		public static object Main(string operation, params object[] args){
			if(operation == "post"){
				byte[] quizBytes = (byte[])args[0];
				return Post(quizBytes);
			}
			if(operation == "getQuiz"){
				BigInteger quizID = (BigInteger)args[0];
				return GetQuiz(quizID);
			}
			return false;

		}
        
		private static BigInteger Post(byte[] quizBytes){
			//[TODO]
			return 0;
		}

        

		private static byte[] GetQuiz(BigInteger quizID){
			//[TODO]
			return new byte[1];
		}

		private static byte[] GetQuizMessage(BigInteger quizID)
        {
			byte[] quizData = GetQuiz(quizID);
			Quiz quiz = DeserializeToQuiz(quizData);
			return new byte[1];
        }

		private static byte[] GetQuizInquierer(BigInteger quizID)
        {
			return new byte[1];
        }

		private static byte[] GetQuizAnswerer(BigInteger quizID)
        {
			return new byte[1];
        }


		private static byte[] GetQuestionTitle(BigInteger questionID){
			return new byte[1];
		}

		private static byte[] GetQuestionAnswer(BigInteger questionID)
        {
			return new byte[1];
        }




    }
}
