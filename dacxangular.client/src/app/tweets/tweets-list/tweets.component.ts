import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Tweet } from '../../_models/tweet'
import { Router } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.css']
})

export class TweetsComponent implements OnInit {
  public tweets: Tweet[] = [];
  constructor(
    private accountService: AccountService,
    private http: HttpClient,
    private router: Router) { }
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
    this.router.navigateByUrl("/tweets-add");
  }

  openEdit(id: number) {
    this.router.navigateByUrl('/tweets-edit/' + id , { state: { id: id } });
  }
}
