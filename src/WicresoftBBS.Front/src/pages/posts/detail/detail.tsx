import React, { useEffect, useState } from 'react';
import { history, useParams } from "umi";
import './detail.less';
import { Button, Row, Col } from 'antd';
import { gePostDetail } from '@/services/api';
export default function Page() {
  const params = useParams();
  const [post, setPost] = useState<any>()

  async function refresh() {
    try {
      const response = await gePostDetail(params.postId as string);
      if (response.status === 200) {
        setPost(response.result)
      } else {
        setPost(null);
      }
    } catch (err) {
      console.error(err);
    }
  }

  useEffect(() => {
    refresh();
  }, [])

  if (post === null) {
    return <div>Post with ID {params.postId} not found.</div>
  }

  return <div className="max-w-screen overflow-x-hidden">
    {post === undefined && <div
      className="fixed w-screen h-screen flex justify-center items-center">
      <p className="animate-pulse">Loading...</p>
    </div>}
    {post && <>
      <div className="flex justify-center">
        <div className="container">

          <Row>
            <Col span={24}>
              <div className='post-title'>qwdwqdqwwd</div>
            </Col>
          </Row>
          <Row>
            <Col className='post-left'>1</Col>
            <Col flex='auto'>
              <Row>
                <Col flex='auto' className='post-content-time'>发表于 {post.createdTime} </Col>
              </Row>
              <Row>
                <Col>{post.content}</Col>
              </Row>
            </Col>
          </Row>
        </div>
      </div>
    </>}
  </div>
}
