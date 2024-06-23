import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Tweet } from '../../_models/tweet'
import { Router } from '@angular/router';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.css']
})

export class TweetsComponent implements OnInit {
  public tweets: Tweet[] = [];
  constructor(private http: HttpClient, private router: Router) { }
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

  gotoTweet() {
    this.router.navigateByUrl("/tweets-form")
  }
}
