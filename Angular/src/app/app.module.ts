import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { QuizComponent } from './quiz/quiz.component';
import { HttpClientModule } from '@angular/common/http';
import { QuizCreateComponent } from './quiz-create/quiz-create.component';

const routes: Routes = [
  { path: 'quiz-create', component: QuizCreateComponent },
  { path: 'quiz-answer', component: QuizComponent },
  { path: '', redirectTo: '/quiz-answer', pathMatch: 'full'},
  { path: '**', redirectTo: '/quiz-answer' }
];


@NgModule({
  declarations: [
    AppComponent,
    QuizComponent,
    QuizCreateComponent
  ],
  imports: [
    NgbModule.forRoot(),
    RouterModule.forRoot(routes, { useHash: true }),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
