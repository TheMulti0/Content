import { INewsItem } from "../models/INewsItem";

export class NewsService {
  private readonly baseUrl = 'http://localhost:5000';

  getNews(): Promise<INewsItem[]> {
    return fetch(`${this.baseUrl}/news`)
      .then(response => response.json())
  }
}
