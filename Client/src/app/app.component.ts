import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProductComponent } from './components/product/product.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,ProductComponent],
  templateUrl: './app.component.html',
 
})
export class AppComponent {
  title = 'Client';
}
