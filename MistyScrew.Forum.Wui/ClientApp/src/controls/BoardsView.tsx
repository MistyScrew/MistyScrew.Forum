import React, { Component } from 'react';
import { Area, Board } from '../api/forum';
import { Table } from 'reactstrap';
import { oc } from 'ts-optchain';
import moment from 'moment';
import { Link } from 'react-router-dom';
import './BoardsView.css';

export function BoardsView(props: { areas: Area[] }) {
  const { areas } = props;
  return (
    <Table>
      <tbody>
        {
          areas.map((area, k) => {
            const areaRow = <tr key={k}><td><b>{area.name}</b></td></tr>;

            const boardRows = area.boards.map((board, boardK) =>
              <tr key={`${k}-${boardK}`} className='board'>
                <td>
                  <Link to={`board/${oc(board).name('')}`}>{oc(board).title('')}</Link><br />
                  <span className='description'>{oc(board).description('')}</span>
                </td>
                <td className='td-threadcount'>
                  {oc(board).state.threadCount()}
                </td>
                <td className='td-postcount'>
                  {oc(board).state.postCount()}
                </td>
                <td className='td-lastpost'>
                  {moment(oc(board).state.lastPost.time('')).format("DD.MM.YYYY HH:mm:ss")}
                </td>
                <td className='td-moderators'>
                  {
                    oc(board).moderators([])
                      .map((moderator, k) => (
                        [
                          k == 0 ? '' : ', ',
                          <Link key={k} className='moderator' to={`user/${oc(moderator).name('')}`}>{oc(moderator).name('')}</Link>
                        ]))
                  }
                </td>
              </tr>
            );

            return [areaRow, ...boardRows];
          })
        }
      </tbody>
    </Table>
    );
}
