import React, { Component } from 'react';
import axios, { AxiosResponse } from 'axios';
import { Area, Board } from '../api/forum';
import { Table } from 'reactstrap';

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

  render () {
    return (
        <div>
          <p>разделы</p>
          <Table>
              <tbody>
                  {
                      this.state.areas.map((area, k) => {
                          const areaRow = <tr key={k}><td><b>{area.name}</b></td></tr>;

                          const boardRows = area.boards.map((board, boardK) =>
                              <tr key={`${k}-${boardK}`}>
                                  <td>
                                      {board.title}{' '}{board.description}
                                  </td>
                              </tr>
                          );

                          return [areaRow, ...boardRows];
                      })
                  }
              </tbody>
          </Table>
        </div>
    );
  }
}

interface MainState {
    areas: Area[];
}