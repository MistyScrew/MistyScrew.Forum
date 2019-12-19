import React, { Component } from 'react';
import { Area, Board } from '../api/forum';
import { Table } from 'reactstrap';
import { oc } from 'ts-optchain';
import moment from 'moment';
import { Link } from 'react-router-dom';
import './BoardsView.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

import { library } from '@fortawesome/fontawesome-svg-core'
import { faLightbulb as faSolidLightbulb} from '@fortawesome/free-solid-svg-icons'
import { faLightbulb as faRegularLightbulb } from '@fortawesome/free-regular-svg-icons'

library.add(faSolidLightbulb, faRegularLightbulb);

function ReadingBulb({ isFlashed }: { isFlashed: boolean }) {
    const icon = isFlashed ? faSolidLightbulb : faRegularLightbulb;
  return <FontAwesomeIcon className='active' icon={icon} />;
}

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
                  <span className='readingbulb'><ReadingBulb isFlashed={oc(board).state.isFlashed(false)} /></span>
                  <Link to={`${oc(board).name('')}`}>{oc(board).title('')}</Link><br />
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
