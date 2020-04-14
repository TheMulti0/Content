import React from 'react';
import { NewsService } from "../../services/NewsService";
import { INewsItem } from "../../models/INewsItem";
import NewsItem from "./NewsItem";
import { Grid } from '@material-ui/core';

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
        <Grid container spacing={2}>
          {
            this.state.items.map(GridNewsItem)
          }
        </Grid>

      </div>
    );

    function GridNewsItem(item: INewsItem, index: number) {
      return (
        <Grid item key={index}>
          <NewsItem item={item}/>
        </Grid>
      );
    }
  }
}
