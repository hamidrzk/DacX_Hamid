import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface Member {
  "id": number,
  "name": string,
  "email": string
}


interface Tweet {
  "id": number
  "message": string,
  "memberId": number,
  "postDate": Date,
  "sender": Member
}


@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.css']
})

export class TweetsComponent implements OnInit {
  public tweets: Tweet[] = [];
  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.getTweets();
  }

  getTweets() {
    this.http.get<Tweet[]>('/api/tweets').subscribe(
      (result) => {
        this.tweets = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }  
}
