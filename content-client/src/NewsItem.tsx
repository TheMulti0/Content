import './NewsItem.css';
import React from 'react';

import { Card, CardHeader, CardContent, Typography, CardMedia } from "@material-ui/core";
import { INewsItem } from "./INewsItem";

export default function NewsItem(props: {item: INewsItem}) {
  const { item } = props;
  return (
    <Card className="Card"
          variant="elevation">

      <CardHeader
        avatar={ <img src={ item.author.imageUrl } /> }
        title={ item.author.name } />

      <CardContent>

        <Typography color="textSecondary">
          { item.title } { item.title === undefined && " | " } { new Date(item.date).toDateString() }
        </Typography>

        <Typography variant="h5">
          { item.description }
        </Typography>

      </CardContent>

      {
        item.imageUrl !== null &&
        <CardMedia
          style={{height: 0, paddingTop: '56.25%'}}
          image={ item.imageUrl } />
      }

    </Card>
  );
}
