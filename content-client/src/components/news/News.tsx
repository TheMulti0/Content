import React from 'react';
import { NewsService } from "../../services/NewsService";
import { INewsItem } from "../../models/INewsItem";
import NewsItem from "./NewsItem";

interface State {
  items: INewsItem[];
}

export default class News extends React.Component<any, State> {
  private newsService: NewsService;

  constructor(props: any) {
    super(props);

    this.newsService = new NewsService();
    this.state = { items: [] };
  }

  async componentDidMount() {
    const items: INewsItem[] = await this.newsService.getNews();
    this.setState({ items })
  }

  render() {
    return (
      <div>
        {
          this.state.items.map(
            (item, index) => <NewsItem key={index} item={item} />)
        }
      </div>
    );
  }
}
