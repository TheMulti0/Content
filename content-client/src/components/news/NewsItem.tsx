import './NewsItem.css';
import React from 'react';

import { Card, CardHeader, CardContent, Typography, CardMedia } from "@material-ui/core";
import { INewsItem } from "../../models/INewsItem";

export default function NewsItem(props: {item: INewsItem}) {
  const { item } = props;
  const date = new Date(item.date);

  function getDate(): string {
    const now = new Date(Date.now());
    let today = now.getDay();

    const day = date.getDay();

    if (day === today) {
      return 'היום';
    }
    if (day === today - 1 || (day === 0 && today === 6)) {
      return 'אתמול';
    }

    return date.toLocaleDateString();
  }

  function getTime(): string {
    const minutes = date.getMinutes();
    let minutesString = minutes.toString();
    if (minutes < 10) {
      minutesString = `0${minutesString}`;
    }
    return `${ date.getHours() }:${ minutesString }`
  }

  return (
    <Card className="card"
          variant="elevation">

      <CardHeader
        avatar={ <img src={ item.author.imageUrl }  alt="" /> }
        title={ item.author.name } />

      <Typography className="date" color="textSecondary" variant="h6">
        { getDate() } בשעה { getTime() }
      </Typography>

      <CardContent>

        <Typography className="content">
          <a href={item.url}>
            { item.description }
          </a>
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
