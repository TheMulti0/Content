import { IAuthor } from "./IAuthor";

export interface INewsItem {
  title: string;
  description: string;
  author: IAuthor;
  date: string;
  url: string;
  imageUrl: string;
  videoUrl: string;
}

