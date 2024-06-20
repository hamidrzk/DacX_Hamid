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
    this.myForm();
  }

  //Create required field validator for name
  myForm() {
    this.tweetForm = this.fb.group({
      tweetId: [{ value: 0, disabled: true }, Validators.required],
      message: ['', Validators.required],
      email: ['', Validators.required]
    });
    //this.tweetForm.controls['tweetId'].disable();
  }

  model: Tweet = {
    message:'',
    "id": 0,
    "memberId": 0,
    "postDate": new Date(),
    "sender": {
      "id": 0,
      "name": '',
      "email": 'Email1742@dac.test'     
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
    //console.log("this.model=" + JSON.stringify(this.model));
    this.http.post<Tweet>('/api/tweets/send', this.model).subscribe(
      (result) => {
        window.location.reload();
        //this.model.id = result.id;
        //this.model.sender.id = result.sender.id;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
