import { Component, OnInit } from '@angular/core';
import { QuizService } from '../services/quiz.service';
import { FormGroup, FormControl, ValidatorFn, AbstractControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-quiz-create',
  templateUrl: './quiz-create.component.html',
  styleUrls: ['./quiz-create.component.css'],
  providers: [QuizService]

})
export class QuizCreateComponent implements OnInit {
  mode = 'enterqueans';
  entryForm: any;
  quizForm: any;
  quizURL: string;
  maxQuizQuestions: Number = 5;
  quiz: any = {
  };
  pager = {
    index: 0,
    size: 1,
    count: 1
  };
  constructor(private quizService: QuizService) {
    this.quiz.options = [];
  }

  ngOnInit() {

    this.createEntityForm();
    this.createQuizForm(false);
  }

  saveQuestion() {

    if (this.quizForm.valid) {
      this.quiz.options.push(this.quizForm.value);
      this.createQuizForm(true);
    } else {
      console.log('invalid form');
    }

  }
  submitQuiz() {

    if (this.quizForm.valid) {
      this.quiz.options.push(this.quizForm.value);
      this.quizService.submitQuiz(this.quiz).subscribe(res => { 
        let response: any = res;
        this.quizURL = location.href.replace(location.hash, '') + '#/quiz-answer?hash=' + response.random_hash;
        this.mode = 'showurl';
        console.log(res); 

      });


    } else {
      console.log('invalid form');
    }

  }

  createQuizForm(increaseIndex: boolean) {
    this.quizForm = new FormGroup({
      questionName: new FormControl(''),
      option1: new FormControl(''),
      option2: new FormControl(''),
      option3: new FormControl(''),
      option4: new FormControl(''),
      answer: new FormControl('')
    });

    if (increaseIndex === true) {
      this.pager.index = this.pager.index + 1;
    }
    this.goTo(this.pager.index);

  }
  saveChanges() {

    if (this.entryForm.valid) {

      this.quiz.name = this.entryForm.controls.fullName.value;

      this.createQuizForm(true);

    } else {
      console.log('invalid form');
    }
  }

  goTo(index: number) {
    if (index >= 0 && index < this.pager.count) {
      this.pager.index = index;
      this.mode = 'enterqueans';
    }
  }


  createEntityForm() {
    this.entryForm = new FormGroup({
      fullName: new FormControl('')
    });


  }

}
