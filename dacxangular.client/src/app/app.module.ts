import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AppRoutingModule } from './app-routing.module';
import { TweetsComponent } from './tweets/tweets-list/tweets.component';
import { TweetsFormComponent } from './tweets/tweets-form/tweets-form.component';
import { MembersComponent } from './members/members.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MembersFormComponent } from './members/members-form/members-form.component';
import { MembersDeleteComponent } from './members/members-delete/members-delete.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    TweetsComponent,
    MembersComponent,
    HomeComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TweetsFormComponent,
    MembersFormComponent,
    MembersDeleteComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
