import { Component } from '@angular/core';
import { TranslateService } from './services/translate.service';
import { forkJoin } from 'rxjs';
import { NysaWords } from 'src/assets/data/words';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Multilingual';

  constructor(private translateService: TranslateService) { }

  step: number = 0;
  allWords: string[] = NysaWords;

  clickTab(stepValue: number){
    this.step = stepValue;
  }

  english: string = "doc/english/-Neo6vnhdwhyfzNFP1jY"
  hindi: string = "doc/hindi/-NenZxVH-3Ly9qkTYjL1"
  marathi: string = "doc/marathi/-NenaN2XhGXKc0kuhC0r"
  tamil: string = "doc/tamil/-NenaRGPXCMJIrnploD5"
  telugu: string = "doc/telugu/-NenaU_Svgbs_3Vjkebd"
  gujarati: string = "doc/gujarati/-NenacXmH3F3Jr09PGFg"

  selectedOption: string = '';

  TranslateAll(pathLanguage: string) {
    this.translateService.getTranslatedData(pathLanguage).subscribe({
      next: (response) => {
        console.log(response);
      }
    })

  }

  onChangeHandler(event: any) {
    this.selectedOption = event.target.value
    this.TranslateAll(this.selectedOption);

  }

  

}
