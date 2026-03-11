import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HardwareService } from './services/hardware.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'app-wpf';

  constructor(private hardware: HardwareService) {}

  ngOnInit() {}

  async ports() {
    const result = await this.hardware.getPorts();
    console.log(result);
  }

  async print() {
    const ticket = `

MI EMPRESA S.A.
RUC: 0999999999001
-------------------------------
FACTURA: 001-001-000000123
Fecha: 10/03/2026
Cliente: Juan Perez
-------------------------------
Producto        Cant    Total
Coca Cola         2      2.00
Pan               1      0.50
-------------------------------
TOTAL                    2.50

Gracias por su compra

        `;

    const result = await this.hardware.print(ticket);

    console.log(result);
  }
}
