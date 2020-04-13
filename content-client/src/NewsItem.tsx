import './NewsItem.css';
import React from 'react';

import { Card, CardHeader, CardContent, Typography, CardMedia } from "@material-ui/core";
import { INewsItem } from "./INewsItem";

export default function NewsItem(props: {item: INewsItem}) {
  const { item } = props;
  return (
    <Card className="Card" variant="elevation">
      <CardHeader
        avatar={ <img src={ item.author.imageUrl }  alt="image" /> }
        title={ item.author.name }
      />
      <CardMedia image={ item.imageUrl } title="image " />
      <CardContent>
        <Typography color="textSecondary">
          { item.title } | { new Date(item.date).toDateString() }
        </Typography>
        <Typography variant="h5" component="h2">
          { item.description }
        </Typography>
      </CardContent>
    </Card>
  );
}
