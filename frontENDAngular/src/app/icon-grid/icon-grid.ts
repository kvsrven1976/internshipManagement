import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-icon-grid',
  imports: [CommonModule],
  templateUrl: './icon-grid.html',
  styleUrl: './icon-grid.css'
})
export class IconGrid {
  icons = [
    { icon: 'bi-bookmark-fill', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-cpu-fill', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-calendar3', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-house-fill', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-speedometer2', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-people-fill', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-geo-alt-fill', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-tools', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' },
    { icon: 'bi-tools', title: 'Featured title', text: 'Paragraph of text beneath the heading to explain the heading.' }
  ];
}
