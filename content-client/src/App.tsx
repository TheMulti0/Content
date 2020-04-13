import React from 'react';
import './App.css';
import { NewsService } from "./NewsService";
import { NewsItem } from "./NewsItem";

interface State {
  items: NewsItem[];
}

export default class App extends React.Component<any, State> {
  private newsService: NewsService;

  constructor(props: any) {
    super(props);

    this.newsService = new NewsService();
    this.state = { items: [] };
  }

  async componentDidMount() {
    const items: NewsItem[] = await this.newsService.getNews();
    this.setState({ items })
  }

  render() {
    return (
      <div>
        { this.state.items.map(item => <p>{item.title}</p>) }
      </div>
    );
  }
}
