import React from 'react';

import { Button, Card, CardHeader, CardContent, Typography } from "@material-ui/core";
import { INewsItem } from "./INewsItem";

export default function NewsItem(props: {item: INewsItem}) {
  const { item } = props;
  return (
    <Card>
      <CardHeader
        avatar={ <img src={ item.imageUrl } alt="s"/> }
        title={ item.title }
        subheader={ item.description }
      />
      <CardContent>
        <Typography color="textSecondary" gutterBottom>
          Word of the Day
        </Typography>
        <Typography variant="h5" component="h2">
          dsa
        </Typography>
        <Typography color="textSecondary">
          adjective
        </Typography>
        <Typography variant="body2" component="p">
          well meaning and kindly.
          <br />
          {'"a benevolent smile"'}
        </Typography>
      </CardContent>
    </Card>
  );
}
