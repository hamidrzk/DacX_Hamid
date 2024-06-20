import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
  selector: 'app-tweets-form',
  templateUrl: './tweets-form.component.html',
  styleUrls: ['./tweets-form.component.css']
})

export class TweetsFormComponent implements OnInit {

  //Create FormGroup
  tweetForm: FormGroup;
  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.initForm();
  }

  emailControl: FormControl;
  messageControl: FormControl;
  alertControl: FormControl;
  nameControl: FormControl;
  initForm() {
    this.tweetForm = this.fb.group({
      message: ['', Validators.required],
      email: ['', Validators.required,Validators.email],
      name: [''],
    });
    this.emailControl = new FormControl('', [Validators.required, Validators.email]);
    this.messageControl = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(140)]);
    this.nameControl = new FormControl("");
    this.alertControl = new FormControl({ value: '', disabled: true });
  }
  alertText: string = '';
  model: Tweet = {
    message:'',
    "id": 0,
    "memberId": 0,
    "postDate": new Date(),
    "sender": {
      "id": 0,
      "name": '',
      "email": ''     
    }
  };

  submitted = false;
  submitForm() {
    this.submitted = true;
    this.postTweet();
  }

  ngOnInit(): void {
  }

  postTweet() {
    console.log("this.model=" + this.model);
    this.http.post<Tweet>('/api/tweets/send', this.model).subscribe(
      (result) => {
        window.location.reload();
      },
      (error) => {
        console.error(error);
        this.alertText = "An error happened during the sending message";
      }
    );
  }
}
