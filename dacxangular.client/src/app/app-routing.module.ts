import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { MembersFormComponent } from './members/members-form/members-form.component';
import { MembersDeleteComponent } from './members/members-delete/members-delete.component';
import { TweetsComponent } from './tweets/tweets-list/tweets.component';
import { TweetsFormComponent } from './tweets/tweets-form/tweets-form.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

const routes: Routes = [
  { path: '', component: HomeComponent },

  { path: 'members', component: MembersComponent },
  { path: 'members-form', component: MembersFormComponent },
  { path: 'members-delete', component: MembersDeleteComponent },

  { path: 'tweets-form', component: TweetsFormComponent },
  { path: 'tweets', component: TweetsComponent },

  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
