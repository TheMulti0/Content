export interface IRouteMapping {

  name: string;
  path: string;
  component: any;
  mappings?: IRouteMapping[];

}
