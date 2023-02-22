import { Component, OnInit } from "@angular/core";
import { ApiService } from '../../services/api.service';
import { SimulatorData } from '../../interfaces/simulator-data';
import { SimulatorResultsData } from '../../interfaces/simulator-results-data';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-simulator",
  templateUrl: "simulator.component.html"
})

export class SimulatorComponent implements OnInit {
  simulatorData: SimulatorData = { initialValue: 0, termInMonths: 0 };
  simulatorResultsData: SimulatorResultsData = { value: 0, valueWithTax: 0 };
  staticAlertClosed = false;
  staticAlertClosed1 = false;
  staticAlertClosed2 = false;
  staticAlertClosed3 = false;
  staticAlertClosed4 = false;
  staticAlertClosed5 = false;
  staticAlertClosed6 = false;
  staticAlertClosed7 = false;

  constructor(private apiService: ApiService, private toastr: ToastrService) { }

  simulatePerformance(simulatorForm) {
    //if (simulatorForm.valid) {  
    this.apiService.simulatePerformance(this.simulatorData).subscribe((
      data: SimulatorResultsData) => {
      this.simulatorResultsData = data;
    },
      error => {
        switch (error.error.status) {
          case 404:
            this.showNotification('Página Não encontrada.', 'top', 'right');
            break;
          case 400:
            for (let key in error.error.errors) {
              error.error.errors[key].map(errorMessage => {
                this.showNotification(errorMessage, 'top', 'right');
              });
            }
            break;
          default:
            this.showNotification('Ocorreu um erro ao processar a requisição.', 'top', 'right');
            break;
        }
      });
    //}
  }

  showNotification(message, from, align) {
    this.toastr.error(`<span class="tim-icons icon-bell-55" [data-notify]="icon"></span> ${message}.`, '', {
      disableTimeOut: true,
      enableHtml: true,
      closeButton: true,
      toastClass: "alert alert-danger alert-with-icon",
      positionClass: 'toast-' + from + '-' + align
    });
  }

  ngOnInit() { }
}
