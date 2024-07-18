// src/components/AddtaskModal.jsx
import React, { useState } from 'react';
import { 
  Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton,
  Button, Input, Textarea, Select, useDisclosure 
} from '@chakra-ui/react';

const CreateTaskModal = ({isOpen, onClose, onCreate, projects, employees  }) => {
  const [task, setTask] = useState({
    projectId: '',
    name: '',
    authorId: '',
    executorId: '',
    comment: '',
    status: '',
    priority: ''
  });
  const handleChange = (e) => {
    const { name, value } = e.target;
    setTask(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = () => {
    const updatedTask = {
      ...task,
      projectId: parseInt(task.projectId), // ensure projectId is an integer
      status: parseInt(task.status), // ensure status is an integer
      priority: parseInt(task.priority) // ensure priority is an integer
    };
    onCreate(updatedTask);
    onClose();
  };

  return (
    <>
      <Button onClick={isOpen} colorScheme="teal" mb={4}>Add task</Button>

      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Add New Task</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
          <Select
                        placeholder='Select a project'
                        name='projectId'
                        value={task.projectId}
                        onChange={handleChange}
                    >
                        {projects?.map((project) => (
                            <option key={project.id} value={project.id}>{project.name}</option>
                        ))}
                    </Select>
            <Input
              placeholder='Name'
              name='name'
              value={task.name}
              onChange={handleChange}
              mb={3}
            />
            <Select
                placeholder='Select a Author'
                name='authorId'
                value={task.authorId}
                onChange={handleChange}
            >
                {employees?.map((employee) => (
                    <option key={employee.id} value={employee.id}>{employee.email}</option>
                ))}
            </Select>
            <Select
                placeholder='Select a Executor'
                name='executorId'
                value={task.executorId}
                onChange={handleChange}
            >
                {employees?.map((employee) => (
                    <option key={employee.id} value={employee.id}>{employee.email}</option>
                ))}
            </Select>
            <Textarea
              placeholder='Comment'
              name='comment'
              value={task.comment}
              onChange={handleChange}
              mb={3}
            />
            <Select
              placeholder='Select status'
              name='status'
              value={task.status}
              onChange={handleChange}
              mb={3}
            >
              <option value="1">To Do</option>
              <option value="2">In progress</option>
              <option value="2">Done</option>
            </Select>
            <Select
              placeholder='Select priority'
              name='priority'
              value={task.priority}
              onChange={handleChange}
              mb={3}
            >
              <option value="1">Priority 1</option>
              <option value="2">Priority 2</option>
              <option value="3">Priority 3</option>
              <option value="4">Priority 4</option>
              <option value="5">Priority 5</option>
            </Select>
          </ModalBody>
          <ModalFooter>
            <Button colorScheme="blue" mr={3} onClick={handleSubmit}>Add</Button>
            <Button variant="ghost" onClick={onClose}>Cancel</Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
};

export default CreateTaskModal;
