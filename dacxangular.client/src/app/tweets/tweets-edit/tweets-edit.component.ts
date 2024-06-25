/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'
import { AbstractControl, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { Tweet } from '../../_models/tweet';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-tweets-edit',
  templateUrl: './tweets-edit.component.html',
  styleUrls: ['./tweets-edit.component.css']
})

export class TweetsEditComponent implements OnInit {
  //Create FormGroup
  tweetForm: FormGroup = new FormGroup({
    message: new FormControl(''),
  });

  state$: Observable<object>;

  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private activatedRoute: ActivatedRoute,
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
        email: ['', [Validators.required, Validators.email]],
        name: ['', Validators.required],
      }
    );
  }

  alertText: string = '';
  submitted: boolean = false;
  formState: object = {};

  model: Tweet = {
    message: '',
    "id": 0,
    "memberId": 0,
    "postDate": new Date(),
    "sender": {} as Member,
  };

  submitForm() {
    this.submitted = true;
    this.updateTweet();
  }

  sub: any;
  ngOnInit(): void {
    this.sub = this.activatedRoute.paramMap.subscribe((params) => {
      console.log(params);
      const id = params.get('id');
      if (id) {
        this.getTweet(id);
      }
    });
  }

  getTweet(id: string | null) {
    this.http.get<Tweet>("/api/tweets/" + id).subscribe(
      (result) => {
        this.model = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  updateTweet() {
    this.alertText = "";
    console.log("this.model=" + this.model);
    this.http.put<Tweet>('/api/tweets/update', this.model).subscribe(
      (result) => {
        if (result) {
          this.alertText = "The tweet is successfully updated";
          setTimeout(() => {
            this.router.navigateByUrl('/tweets');
          }, 2000);
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
