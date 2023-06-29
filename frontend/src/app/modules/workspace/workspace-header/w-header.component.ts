import { Component, OnInit, Input } from '@angular/core';
import {Router} from '@angular/router';

import { UserDTO } from 'src/app/models/user/user-dto';

@Component({
  selector: 'app-w-header',
  templateUrl: './w-header.component.html',
  styleUrls: ['./w-header.component.css']
})
export class WorkspaceHeaderComponent implements OnInit {
  
  constructor(private router: Router) {}

  @Input() currentUser: UserDTO = {} as UserDTO;

  ngOnInit(): void {
      
  }

  public redirectToLogin()
  {
    this.router.navigate(['./auth']);
  }
}
