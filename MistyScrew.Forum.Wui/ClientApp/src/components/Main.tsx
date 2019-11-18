import React, { Component } from 'react';
import axios, { AxiosResponse } from 'axios';
import { Area, Board } from '../api/forum';
import { Table } from 'reactstrap';
import { BoardsView } from '../controls/BoardsView';

export class Main extends Component<{}, MainState> {
  static displayName = Main.name;

  constructor(props: any) {
    super(props);

    this.state = { areas: [] };

    this.load();
  }

  load = async () => {
    const response: AxiosResponse<Area[]> = await axios.get('api/forum/areas');
    this.setState({ areas: response.data });
  }

  render() {
    return (
      <div>
        <p>Разделы</p>
        <BoardsView areas={this.state.areas}/>

      </div>
    );
  }
}

interface MainState {
    areas: Area[];
}