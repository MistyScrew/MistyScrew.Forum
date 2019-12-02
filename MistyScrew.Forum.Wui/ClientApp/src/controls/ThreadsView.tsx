import React, { Component } from 'react';
import { Thread } from '../api/forum';
import { Table } from 'reactstrap';
import { oc } from 'ts-optchain';
import moment from 'moment';
import { Link } from 'react-router-dom';
import './BoardsView.css';

export function ThreadsView(props: { threads: Thread[] }) {
  const { threads } = props;
  return (
    <Table>
      <tbody>
        {
          threads.map((thread, k) => (

              <tr key={k} className='thread'>
                <td>
                  <Link to={`/thread/${oc(thread).id('')}`}>{oc(thread).title('')}</Link>
                </td>
              </tr>
          ))
        }
      </tbody>
    </Table>
    );
}
