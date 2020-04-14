import './NewsItem.css';
import React from 'react';

import { Card, CardHeader, CardContent, Typography, CardMedia } from "@material-ui/core";
import { INewsItem } from "../../models/INewsItem";

export default function NewsItem(props: {item: INewsItem}) {
  const { item } = props;
  return (
    <Card className="card"
          variant="elevation">

      <CardHeader
        avatar={ <img src={ item.author.imageUrl }  alt="" /> }
        title={ item.author.name } />

      <Typography className="date" color="textSecondary" variant="h6">
        { new Date(item.date).toDateString() }
      </Typography>

      <CardContent>

        <Typography className="content">
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
