using System;
using System.Text;

using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;

using Neunity.Tools;

#if NEOSC
using Neunity.Adapters.NEO;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;
#else
using Neunity.Adapters.Unity;
#endif


/*
 * @Introduction
 * 'How much you know about me' is a NEO-based blockchain game with following flow:
 * 1. The girl goes to our website to create some quiz, a invitation URL would be generated after submission. 
 * 2. The girl sends the invitation URL to the boy. The boy must correctly answer all the quiz correctly in order to get a chance to post a message to the girl onto blockchain.
 * 3. The message includes all the Q&A in this game, a long with the boy's message to her. It will stay on blockchain eternally for their love.
 * 
 */
using System.Data;

namespace Howmuch
{
	public class Howmuch: SmartContract
    {
        
        //[S<title>,S<answer>]
		public class Question{
            
			public string title;    //The title of a question
            public string answer;   //The correct answer of this quesion. Only the correct answr would be recorded onto blockchain.
		}

        //[S<nameInquirer>,S<nameAnswerer>,S<message>,S[<questionIDs>*]]
		public class Quiz{
            public byte[] quizId;
			public byte[] nameInquirer;     //The name of the quiz inquirer   
			public byte[] nameAnswerer;     //The name of the quiz answerer
            public string message;          //The message that answerer gives to the inquierer
            public Question[] questions;    //All the questions
		}

        public static Quiz Bytes2Quiz(byte[] data){
            Quiz quiz = new Quiz
            {
                quizId = data.SplitTbl(0),
                nameInquirer = data.SplitTbl(1),
                nameAnswerer = data.SplitTbl(2),
                message = data.SplitTblStr(3)
            };
            byte[] dataQuestions = data.SplitTbl(4);
            int numQuestions = dataQuestions.SizeTable();
            quiz.questions = new Question[numQuestions];
            for (int i = 0; i < numQuestions;i++){
                quiz.questions[i] = Bytes2Question(dataQuestions.SplitTbl(i));
            }
            return quiz;
        }

        public static byte[] Quiz2Bytes(Quiz quiz){
            byte[] ret = NuSD.Seg(quiz.nameInquirer).AddSeg(quiz.nameAnswerer).AddSegStr(quiz.message);
            byte[] qids = Op.Void;
            for (int i = 0; i < quiz.questions.Length;i++){
                qids = qids.AddSeg(Question2Bytes(quiz.questions[i]));
            }
            return ret.AddSeg(qids);
        }

        public static byte[] Question2Bytes(Question question){
            return NuSD.SegString(question.title).AddSegStr(question.answer);
        }

        public static Question Bytes2Question(byte[] data){
            return new Question
            {
                title = data.SplitTblStr(0),
                answer = data.SplitTblStr(1)
            };

        }
        public static readonly byte[] Owner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();
        public static readonly byte[] GAS_ID = { 231, 45, 40, 105, 121, 238, 108, 177, 183, 230, 93, 253, 223, 178, 227, 132, 16, 11, 141, 20, 142, 119, 88, 222, 66, 228, 22, 139, 113, 121, 44, 96 };
        public static readonly BigInteger gasRequired = 1;

		public static object Main(string operation, params object[] args){
            if (Runtime.Trigger == TriggerType.Verification)
            {
                if (operation == "post")
                {
                    byte[] quizBytes = (byte[])args[0];
                    return Post(quizBytes);
                }
            }
			
			if(operation == "getQuiz"){
				BigInteger quizID = (BigInteger)args[0];
				return GetQuiz(quizID);
			}

            if (operation == "getMessage")
            {
                BigInteger quizID = (BigInteger)args[0];
                return GetQuizMessage(quizID);
            }

            return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.NotFound);   //return 404

		}

        public static BigInteger GetGASAttached()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            foreach (TransactionOutput output in tx.GetOutputs())
            {
                if (output.ScriptHash == ExecutionEngine.ExecutingScriptHash
                    && output.AssetId == GAS_ID)
                    return output.Value;
            }
            return 0;
        }

        public static byte[] GetTransReceiver()
        {
            return ExecutionEngine.ExecutingScriptHash;
        }
        
		private static BigInteger Post(byte[] quizBytes){
            if(GetTransReceiver() == Owner){
                if(GetGASAttached()>=gasRequired){
                    Quiz quiz = Bytes2Quiz(quizBytes);

                    byte[] id = Hash160(quizBytes);
                    quiz.quizId = id;

                    NuIO.SetStorageWithKeyPath(Quiz2Bytes(quiz),"quiz", Op.Bytes2String(id));
                }
            }
			return 0;
		}

        

		private static byte[] GetQuiz(BigInteger quizID){
            byte[] qdata=  NuIO.GetStorageWithKeyPath("quiz", Op.BigInt2String(quizID));
            return NuTP.RespDataSucWithBody(qdata);
		}

		private static byte[] GetQuizMessage(BigInteger quizID)
        {
			byte[] quizData = GetQuiz(quizID);
            if(quizData.Length != 0){
                Quiz quiz = Bytes2Quiz(quizData);
                byte[] message = Op.String2Bytes(quiz.message);
                return NuTP.RespDataSucWithBody(message);
            }
            return NuTP.RespDataWithCode(NuTP.SysDom, NuTP.Code.NotFound);

        }

    }
}
