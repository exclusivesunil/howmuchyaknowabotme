import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class QuizService {
  domain = 'http://localhost'

  constructor(private http: HttpClient) { }

  get(hash: string) {

     let url =this.domain+'/php/getquiz.php?random_hash='+ hash;
    return this.http.get(url);
  }

  submitQuiz(quiz: any) 
  {
    let url =this.domain+'/php/submitquiz.php';

    return this.http.post(url, quiz);
    

  }

    getAll() {
    return [
      { id: 'data/aspnet.json', name: 'Asp.Net' },
      { id: 'data/csharp.json', name: 'C Sharp' },
      { id: 'data/designPatterns.json', name: 'Design Patterns' }
    ];
  }

}
