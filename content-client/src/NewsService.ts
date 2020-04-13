import { NewsItem } from "./NewsItem";

export class NewsService {
  private readonly baseUrl = 'http://localhost:5000';

  getNews(): Promise<NewsItem> {
    return fetch(`${this.baseUrl}/news`)
      .then(response => response.json())
  }
}
