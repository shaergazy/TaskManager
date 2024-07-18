// src/components/EdittaskModal.jsx
import React, { useState, useEffect } from 'react';
import { 
  Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton,
  Button, Input, Textarea, Select, useDisclosure 
} from '@chakra-ui/react';

const EditTaskModal = ({ task, isOpen, onClose, onSave, employees, projects}) => {
  const [editedtask, setEditedtask] = useState({ ...task });

  useEffect(() => {
    setEditedtask({ ...task });
  }, [task]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditedtask(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = () => {
    onSave(editedtask);
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Edit task</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Input
            placeholder='Name'
            name='name'
            value={editedtask.name}
            onChange={handleChange}
            mb={3}
          />
                    <Select
                        placeholder='Select a project'
                        name='projectId'
                        value={editedtask.projectId}
                        onChange={handleChange}
                    >
                        {projects?.map((project) => (
                            <option key={project.id} value={project.id}>{project.name}</option>
                        ))}
                    </Select>
          <Select
                placeholder='Select a Author'
                name='authorId'
                value={editedtask.authorId}
                onChange={handleChange}
            >
                {employees?.map((employee) => (
                    <option key={employee.id} value={employee.id}>{employee.email}</option>
                ))}
            </Select>
            <Select
                placeholder='Select a Executor'
                name='executorId'
                value={editedtask.executorId}
                onChange={handleChange}
            >
                {employees?.map((employee) => (
                    <option key={employee.id} value={employee.id}>{employee.email}</option>
                ))}
            </Select>
          <Textarea
            placeholder='Comment'
            name='comment'
            value={editedtask.comment}
            onChange={handleChange}
            mb={3}
          />
          <Select
            placeholder='Select status'
            name='status'
            value={editedtask.status}
            onChange={handleChange}
            mb={3}
          >
            <option value="1">Status 1</option>
            <option value="2">Status 2</option>
          </Select>
          <Select
            placeholder='Select priority'
            name='priority'
            value={editedtask.priority}
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
          <Button colorScheme="blue" mr={3} onClick={handleSubmit}>Save</Button>
          <Button variant="ghost" onClick={onClose}>Cancel</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default EditTaskModal;
