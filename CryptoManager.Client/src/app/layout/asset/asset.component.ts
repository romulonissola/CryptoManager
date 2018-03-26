import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Asset, AssetService } from '../../shared'
@Component({
  selector: 'app-asset',
  templateUrl: './asset.component.html',
  styleUrls: ['./asset.component.scss'],
  animations: [routerTransition()]
})
export class AssetComponent implements OnInit {
  private assets: Asset[] = [];

  constructor(private translate: TranslateService, private assetService: AssetService) {}

   ngOnInit() {
    this.assetService.getAll()
      .subscribe(data => this.assets = data);
  }

  delete(asset){
    if (confirm(this.translate.instant("DeleteMessage"))) {
      var index = this.assets.indexOf(asset);
      this.assets.splice(index, 1);
      this.assetService.delete(asset.id)
        .subscribe(null,
          err => {
            alert("Could not delete.");
            // Revert the view back to its original state
            this.assets.splice(index, 0, asset);
          });
    }
  }

}
