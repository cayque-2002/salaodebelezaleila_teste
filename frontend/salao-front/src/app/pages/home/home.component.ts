import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  selectedWeek = 4;

  data = [
    { week: 1, value: 10 },
    { week: 2, value: 25 },
    { week: 3, value: 18 },
    { week: 4, value: 40 },
    { week: 5, value: 30 }
  ];

  get filteredData() {
    return this.data.filter(d => d.week <= this.selectedWeek);
  }

  get total() {
    return this.filteredData.reduce((a, b) => a + b.value, 0);
  }
}