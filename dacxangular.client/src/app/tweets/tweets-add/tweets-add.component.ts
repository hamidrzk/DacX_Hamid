import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AbstractControl, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Tweet } from '../../_models/tweet';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-tweets-add',
  templateUrl: './tweets-add.component.html',
  styleUrls: ['./tweets-add.component.css']
})

export class TweetsAddComponent implements OnInit {

  //Create FormGroup
  tweetForm: FormGroup = new FormGroup({
    message: new FormControl(''),
  });

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router) {
    this.initForm();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.tweetForm.controls;
  }

  initForm() {
    this.tweetForm = this.formBuilder.group(
      {
        message: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(140)
          ]
        ],
      }
    );
  }

  alertText: string = '';
  submitted: boolean = false;
  model: Tweet = {
    message:'',
    "id": 0,
    "memberId": 0,
    "postDate": new Date(),
    "sender": {} as Member,
  };

  submitForm() {
    this.submitted = true;
    this.postTweet();
  }

  ngOnInit(): void {
    this.model.sender = this.accountService.getCurrentUser();
  }

  postTweet() {
    this.alertText = "";
    console.log("this.model=" + this.model);
    this.http.post<Tweet>('/api/tweets/send', this.model).subscribe(
      (result) => {
        if (result) {
          //window.location.reload();
          this.router.navigateByUrl('/tweets');
        }
        else {
          this.alertText = result;
        }
      },
      (ex) => {
        console.error(ex);
        this.alertText = ex.error;
        this.submitted = false;
      }
    );
  }
}
