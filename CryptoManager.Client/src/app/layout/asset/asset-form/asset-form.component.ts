import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routerTransition } from '../../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Asset, AssetService } from '../../../shared'

@Component({
  selector: 'app-asset-form  ',
  templateUrl: './asset-form.component.html',
  styleUrls: ['./asset-form.component.scss'],
  animations: [routerTransition()]
})
export class AssetFormComponent implements OnInit {
  title: string;
  asset: Asset = new Asset();
  formState: string;
  constructor(private translate:TranslateService,
              private assetService: AssetService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    var id = this.route.params.subscribe(params => {
      var id = params['id'];

      this.formState = id ? "Edit" : "Add";
      this.title = this.translate.instant(this.formState);

      if (!id)
        return;

      this.assetService.get(id)
         .subscribe(
           asset => this.asset = asset,
           response => {
             if (response.status == 404) {
               this.router.navigate(['NotFound']);
             }
           });
    });
  }

  save() {
    var result;
    if (this.asset.id){
      result = this.assetService.update(this.asset);
    } else {
      result = this.assetService.add(this.asset);
    }

    result.subscribe(data => this.router.navigate(['asset']));
  }

}
