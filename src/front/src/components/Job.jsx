import React from 'react';
import { Box, Heading, Text, Badge, Flex, Spacer, Button } from '@chakra-ui/react';

// Пример статусов задач
const JobStatus = {
  1: 'To Do',
  2: 'In progress',
  3: 'Done'
};

const TaskCard = ({ task, onEdit, onDelete }) => {
  return (
    <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4}>
      <Heading as="h3" size="md" mb={2}>{task.name}</Heading>
      <Text mb={2}>Comment: {task.comment}</Text>
      <Text mb={2}>Status: <Badge colorScheme={task.status === 3 ? 'green' : 'grey'}>{JobStatus[task.status]}</Badge></Text>
      <Text mb={2}>Priority: {task.priority}</Text>
      <Text mb={2}>Author: {task.author}</Text>
      <Text mb={2}>Executor: {task.executor}</Text>
      <Flex>
        <Spacer />
        <Button colorScheme="red" mr={2} onClick={() => onDelete(task.id)}>Delete</Button>
        <Button colorScheme="blue" onClick={() => onEdit(task)}>Edit</Button>
      </Flex>
    </Box>
  );
};

export default TaskCard;
