import React, { Component } from 'react';
import axios, { AxiosResponse } from 'axios';
import { Area, Board_Name, Thread } from '../api/forum';
import { Table } from 'reactstrap';
import { ThreadsView } from '../controls/ThreadsView';

export class BoardView extends Component<BoardProps, BoardViewState> {
  static displayName = BoardView.name;

  constructor(props: BoardProps) {
    super(props);

    this.state = { threads: [] };

    this.load();
  }

  load = async () => {
    const response: AxiosResponse<Thread[]> = await axios.get(`api/forum/board/${this.props.boardName}/threads`);
    this.setState({ threads: response.data });
  }

  render() {
    return (
      <div>
        <ThreadsView threads={this.state.threads} />

      </div>
    );
  }
}

interface BoardProps {
  boardName: string;
}
interface BoardViewState {
    threads: Thread[];
}